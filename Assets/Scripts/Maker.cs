using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maker : MonoBehaviour
{

    public int _currentTile = 1;
    public GameObject _planePrefab;
    public float Time=0.1f;
    public int x;
    public int y;

    private int index=0;

    public List<int> _stacking;
    private int StartingTile;

    private void Start()
    {
        StartingTile = _currentTile;
        MakeTiles();
        AddContainers();
        AddWalls();
         StartGeneratingMaze();

    }

    void MakeTiles()
    {

        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                Vector3 pos;
                pos = new Vector3(i, 0, j);
                GameObject _tile = Instantiate(_planePrefab, pos, Quaternion.identity, transform);
            }
        }

    }

    void AddContainers()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Container _tileContainer = transform.GetChild(i).GetComponent<Container>();
            _tileContainer.index = index;
            _tileContainer.gameObject.name = index.ToString();

            // for up tile
            if (index - y >= 0)
            {
                _tileContainer.U_data._tile = transform.GetChild(index - y);
            }
            
            // for down tile
            if (index + y <= (x*y)-1)
            {
                _tileContainer.D_data._tile = transform.GetChild(index +y);
            }
            
            // for left tile
            if(index % y != 0)
            {
                _tileContainer.L_data._tile = transform.GetChild(index - 1);
            }
            
            // for right tile
            if((index+1) % y != 0)
            {
                _tileContainer.R_data._tile = transform.GetChild(index + 1);
            }
            

            index++;
        }
    }

    void AddWalls()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Container _tileContainer = transform.GetChild(i).GetComponent<Container>();
            GameObject _tileContainerGO = _tileContainer.gameObject;


            // for right wall
            if (_tileContainer.R_data._wall == null)
            {
                GameObject wall = Instantiate(_planePrefab, _tileContainerGO.transform.position + new Vector3(0, 0.5f, 0.5f), Quaternion.Euler(90, 0, 0));
               
                _tileContainer.R_data._wall = wall.transform;
                if ((_tileContainer.index + 1) % y != 0)
                {
                    if (transform.GetChild(i + 1).GetComponent<Container>().L_data._wall == null)
                    {

                        transform.GetChild(i + 1).GetComponent<Container>().L_data._wall = wall.transform;
                    }
                }
            }



            // for down data
            if (_tileContainer.D_data._wall == null)
            {
                GameObject wall = Instantiate(_planePrefab, _tileContainerGO.transform.position + new Vector3(0.5f, 0.5f, 0), Quaternion.Euler(90, 0, 90));
                
                _tileContainer.D_data._wall = wall.transform;
                if (_tileContainer.index + y <= (x * y) - 1)
                {
                    if (transform.GetChild(i + y).GetComponent<Container>().U_data._wall == null)
                    {
                        transform.GetChild(i + y).GetComponent<Container>().U_data._wall = wall.transform;
                    }
                }
            }



            // for up data
            if (_tileContainer.U_data._wall == null)
            {
                GameObject wall = Instantiate(_planePrefab, _tileContainerGO.transform.position + new Vector3(-0.5f, 0.5f, 0), Quaternion.Euler(90, 0, 90));
                
                _tileContainer.U_data._wall = wall.transform;
                if (_tileContainer.index - y >= 0)
                {
                    if (transform.GetChild(i - y).GetComponent<Container>().D_data._wall == null)
                    {
                        transform.GetChild(i - y).GetComponent<Container>().D_data._wall = wall.transform;
                    }
                }
            }



            // for left wall
            if (_tileContainer.L_data._wall == null)
            {
                GameObject wall = Instantiate(_planePrefab, _tileContainerGO.transform.position + new Vector3(0, 0.5f, -0.5f), Quaternion.Euler(90, 0, 0));
                
                _tileContainer.L_data._wall = wall.transform;

                if (_tileContainer.index % y != 0)
                {
                    if (transform.GetChild(i - 1).GetComponent<Container>().R_data._wall == null)
                    {
                        transform.GetChild(i - 1).GetComponent<Container>().R_data._wall = wall.transform;
                    }
                }
            }

        }

    }

    void StartGeneratingMaze()
    {

        
        StartCoroutine(Move());
    }


    IEnumerator Move()
    {

        yield return new WaitForSeconds(Time);
        
        Container _con = transform.GetChild(_currentTile).GetComponent<Container>();
        _con.visited = true;
        _currentTile = DecideNeighbourToGo(_con);




        if (_currentTile == 5000)
        {
            // yield break;

            _con.gameObject.GetComponent<Renderer>().material.color = Color.blue;
            if(_stacking.Count < 1)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    
                    transform.GetChild(i).GetComponent<Renderer>().material.color = Color.green;
                }
                yield break;
            }

            _currentTile = _stacking[_stacking.Count - 1];
            _stacking.RemoveAt(_stacking.Count - 1);




            yield return StartCoroutine(Move());
        }
        else
        {
            _stacking.Add(_con.index);
            _con.gameObject.GetComponent<Renderer>().material.color = Color.red;

            yield return StartCoroutine(Move());
        }

    }

    int DecideNeighbourToGo(Container _con)
    {
        List<string> _s = new List<string>();
        if (CanGoUpFromThis(_con))
        {
            _s.Add("Up");
        }
        if (CanGoDownFromThis(_con))
        {
            _s.Add("Down");
        }
        if (CanGoRightFromThis(_con))
        {
            _s.Add("Right");
        }
        if (CanGoLeftFromThis(_con))
        {
            _s.Add("Left");
        }
        if(_s.Count == 0)
        {
            return 5000;
        }
        string choosingRandom = _s[Random.Range(0, _s.Count)];
        print("Going to " + choosingRandom + " From " + _con.index);
        if (choosingRandom == "Up")
        {
            if (_con.U_data._wall != null)
            {
                Destroy(_con.U_data._wall.gameObject);
            }
            return GetUpIndexNumber(_con);

        }
        else if (choosingRandom == "Down")
        {
            if (_con.D_data._wall != null)
            {
                Destroy(_con.D_data._wall.gameObject);
            }
            return GetDownIndexNumber(_con);
        }
        else if (choosingRandom == "Left")
        {
            if (_con.L_data._wall != null)
            {
                Destroy(_con.L_data._wall.gameObject);
            }
            return GetLeftIndexNumber(_con);
        }
        else if (choosingRandom == "Right")
        {
            if (_con.R_data._wall != null)
            {
                Destroy(_con.R_data._wall.gameObject);
            }
            return GetRightIndexNumber(_con);
        }
        return 5000;
    }

    int GetUpIndexNumber(Container _con)
    {
        return _con.index - y;
    }

    int GetDownIndexNumber(Container _con)
    {
        return _con.index + y;
    }

    int GetRightIndexNumber(Container _con)
    {
        return _con.index + 1;
    }

    int GetLeftIndexNumber(Container _con)
    {
        return _con.index - 1;
    }

    bool CanGoUpFromThis(Container _con)
    {
        if (_con.U_data._tile != null && _con.U_data._tile.GetComponent<Container>().visited==false)
        {
            return true;
        }
        return false;
    }

    bool CanGoDownFromThis(Container _con)
    {
        if (_con.D_data._tile != null && _con.D_data._tile.GetComponent<Container>().visited == false)
        {
            return true;
        }
        return false;
    }

    bool CanGoRightFromThis(Container _con)
    {
        if (_con.R_data._tile != null && _con.R_data._tile.GetComponent<Container>().visited == false)
        {
            return true;
        }
        return false;
    }

    bool CanGoLeftFromThis(Container _con)
    {
        if (_con.L_data._tile != null && _con.L_data._tile.GetComponent<Container>().visited == false)
        {
            return true;
        }
        return false;
    }



}
