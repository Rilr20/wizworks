# Grid Square Generator

This is a full-stack project with a RESTful backend in ASP.NET Core and a React frontend. The application displays colored squares on a grid. Each square is generated with a unique color and saved along with its position. The grid state persists between reloads using a local JSON file.

## 🔧 Backend – ASP.NET Core Web API

### 📦 Endpoints

- `GET /`  
  Health check – returns `"Hello World!"`.

- `GET /squares`  
  Returns the current layout of squares from `squares.json`.  
  **Returns 400** if the file doesn't exist.

- `POST /square/create`  
  Creates a new square with a unique color and a calculated position based on a spiral logic. The square is saved to `squares.json`.

  - If no square is passed, starts at `0,0`.
  - Each new square spirals out from the last.
  - Ensures no two consecutive squares have the same color.

- `POST /square/destroy`  
  Deletes the `squares.json` file, effectively resetting the layout.

### 📁 Data Format (in `squares.json`)

Each square is saved as a JSON object with this format:

```json
{
  "square": "x,y",
  "color": "#rrggbb"
}
```

# 🛠 How to Run

## Backend
uses port `5019`

```
cd backend
dotnet run
```

## Frontend
uses port `3000`

```
cd frontend
npm install
npm start
```