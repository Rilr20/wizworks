import React from 'react'

export default function Display({ squares, width, height }) {
    try {
        let sortedSquares = [...squares].sort((a, b) => {
            const aY = parseInt(a.square.split(",")[1])
            const bY = parseInt(b.square.split(",")[1])

            return aY - bY;
        })

        const rows = [];

        sortedSquares.forEach(({ square, color }, index) => {
            const [x, y] = square.split(",").map(Number);
            console.log(index);

            if (!rows[y]) {
                console.log(rows[y]);
                rows[y] = []

            }
            rows[y].push({ x, y, color })
        })
        function addBuffers(firstRow, lastRow, squares) {
            console.log(lastRow);

            let y = lastRow[0].y;
            let x = Math.min(...lastRow.map(s => s.x));
            console.log(x);
            console.log(y);

            if (firstRow.length !== lastRow.length && x - 1 >= 0) {
                let idx = 1;
                while (lastRow.length !== firstRow.length) {
                    lastRow.unshift({ x: x - idx, y: y, color: "" })
                    idx++
                }
            }
        }
        addBuffers(rows[0], rows[rows.length - 1])

        const sortedRowKeys = Object.keys(rows).map(Number).sort((a, b) => a - b);
        return <div style={{ display: "flex", flexDirection: "column",}}>
                {sortedRowKeys.map((y) => (
                    <div style={{ display: "grid", gridTemplateColumns: `repeat(${rows[0].length}, ${width + 5 + "px"})`, justifyContent:"center" }}>
                        {rows[y]
                            .sort((a, b) => a.x - b.x)
                            .map(({ x, color }, i) => (
                                <div
                                    style={{
                                        width: width,
                                        height: height,
                                        margin: "5px",
                                        backgroundColor: color,
                                        border: color != "" ? "1px solid black" : "1px solid white"
                                    }}
                                ></div>
                            ))}
                    </div>
                ))}
        </div>

    } catch (error) {
        console.log(error);

    }
}


// const Display = ({ squares, width, height }) => {

    

// };

// export default Display;
