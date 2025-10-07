# 🌀 Unity Maze Generator (BFS / DFS Hybrid)

A simple yet powerful **procedural maze generator** built in **Unity (C#)** using a **depth-first search backtracking algorithm** (often described as BFS-style expansion).  
This project demonstrates how to generate perfect mazes in real time — ideal for **game prototyping**, **AI pathfinding visualization**, or **level generation experiments**.

![Demo Screenshot](assets/demo.gif)

---

## 🧩 Features

- Generates mazes dynamically at runtime  
- Visual step-by-step generation using coroutines  
- Modular and easy to extend (`Container` class for tiles/walls)  
- Works with any grid size (X × Y)  
- Reuses Unity prefabs for tiles and walls  
- Simple BFS/DFS traversal logic with randomization for maze variability  

---

## ⚙️ Algorithm Overview

The algorithm used is a **Depth-First Search (DFS)** maze generator, implemented with an explicit stack (`_stacking`).  

### 🧠 Process:
1. Start from a random cell  
2. Choose a random unvisited neighbor  
3. Remove the wall between them  
4. Move to that neighbor and mark it visited  
5. If no unvisited neighbors exist, backtrack using the stack  
6. Repeat until all cells are visited  

This is effectively a **recursive backtracking** algorithm — iterative in this implementation.

---

## ⏱️ Time & Space Complexity

| Operation | Complexity | Explanation |
|------------|-------------|-------------|
| Maze Generation | **O(N)** | Each cell is visited once (N = X × Y) |
| Space Usage | **O(N)** | The `_stacking` list and container grid hold references to all tiles |
| Wall Creation | **O(N)** | Each cell spawns a constant number (≤4) of walls |

**Overall:**  
> ⏰ Time Complexity: `O(X × Y)`  
> 💾 Space Complexity: `O(X × Y)`

## 🧩 How It Works in Unity

1. Attach `Maker.cs` to an empty GameObject in your scene  
2. Assign `_planePrefab` (a simple plane or cube)  
3. Set grid dimensions `x` and `y` (e.g., 10×10)  
4. Adjust `Time` to control generation speed  
5. Run the scene — the maze will build step by step  

Walls and tiles are created dynamically, and color changes (red → blue → green) visualize traversal progress.

---

## 🧑‍💻 Example Inspector Setup

| Field | Description |
|-------|--------------|
| `_planePrefab` | Prefab used for tiles & walls |
| `x`, `y` | Maze dimensions |
| `Time` | Delay between generation steps |
| `_currentTile` | Starting cell index |
| `_stacking` | Internal stack (do not modify) |

---

## 🚀 Usage Ideas

- Procedural dungeon or labyrinth levels  
- Pathfinding algorithm visualization (A*, BFS, DFS, Dijkstra)  
- Educational demos on recursion and graph traversal  
- Puzzle or adventure game prototypes  

---

## 🧰 Dependencies

- Unity **2021.3+**
- No external packages required  

---

## 📈 Future Improvements

- Add entrance/exit generation  
- Use `LineRenderer` for 2D visualizations  
- Optimize for large grid sizes  
- Support 3D maze generation  
- Add GUI controls for regeneration in runtime  

---

## 🤝 Contributing

Contributions, pull requests, and issues are always welcome!  
If you improve the generation logic (e.g., implement Prim’s or Kruskal’s maze algorithms), feel free to open a PR.

---

## 📜 License

This project is licensed under the **MIT License** — you’re free to use it in personal or commercial projects.

---

## 🧠 Author

**Abdul Ahad**  
Unity Developer


