import React, { useState, useEffect } from 'react';
import SquareController from "./components/SquareController";
import Display from './components/Display';
import './App.css';

function App() {
  const [squares, setSquares] = useState([]);

  useEffect(() => {
    const fetchSquares = async () => {
      try {

        const response = await fetch('http://localhost:5019/squares');
        const data = await response.json();
        if (response.status === 200) {
          setSquares(data);
        }
      } catch (error) {
        console.log(error);

      }
    };
    fetchSquares();
  }, []);

  const handleAddSquare = async () => {
    let newSquare
    if (squares.length !== 0) {
      newSquare = squares[squares.length - 1];

    } else {
      newSquare = { square: "", color: "" };

    }

    try {
      const response = await fetch('http://localhost:5019/square/create', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(newSquare),
      });
      const data = await response.json()
      if (response.ok) {

        setSquares([...squares, data]);
      }
    } catch (error) {
      console.log(error);
    }
  };

  const handleClearSquares = async () => {
    const response = await fetch('http://localhost:5019/square/destroy', {
      method: 'POST',
    });

    if (response.ok) {
      setSquares([]);
    }
  };

  return (
    <div className="App">
      <h1>Squares Grid</h1>
      <SquareController onAdd={handleAddSquare} onClear={handleClearSquares} />
      <div className="grid">
        <Display squares={squares} width={40} height={40} />
      </div>
    </div>
  );
}

export default App;
