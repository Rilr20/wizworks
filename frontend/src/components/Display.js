// frontend/src/components/Display.jsx

import React from 'react';

const Display = ({ width, height, colour, pos }) => {
    
    let split = pos.split(",")
    let hexColour = colour.replace("#", '')
    const inverted = (0xFFFFFF - parseInt(hexColour, 16)).toString(16).padStart(6, '0');
    const styles = {
        width: `${width}px`,
        height: `${height}px`,
        backgroundColor: colour,
        position: 'absolute',
        left: `${(parseInt(split[0]) * width) + 15*split[0]}px`,
        top: `${(parseInt(split[1]) * height) + 15* split[1]}px`,
        border: `3px solid #${inverted}`
    };

    return <div style={styles}></div>;
};

export default Display;
