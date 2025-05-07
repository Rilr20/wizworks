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
        console.log(response.status !== 400);
        console.log(data);
        if (response.status === 200) {
          console.log("tja");

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
    console.log(squares);

    if (squares.length !== 0) {
      const latest = squares[squares.length - 1];
      console.log(latest);
      newSquare = { square: latest.square, color: latest.color };
      console.log(newSquare);

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
        {squares.map((square, index) => (
          <Display key={index} width={100} height={100} colour={square.color} pos={square.square} />
        ))}
      </div>
    </div>
  );
}

export default App;
