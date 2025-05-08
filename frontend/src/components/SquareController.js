import React, { useState } from 'react';

export default function SquareController({ onAdd, onClear }) {
    const handleAdd = () => {
        onAdd();
    };

    const handleClear = () => {
        onClear();
    };

    return (
        <div>
            <button class="button" onClick={handleAdd}>Add Square</button>
            <button class="button" onClick={handleClear}>Clear Squares</button>
        </div>
    );
};