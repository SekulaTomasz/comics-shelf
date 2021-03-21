import React from 'react'
import './Style.css';


const Spinner = () => {
    return (
        <div className="spinnerContainer">
            <div className="spinnerOverlay"></div>
            <div className="lds-roller"><div></div><div></div><div></div><div></div><div></div><div></div><div></div><div></div></div>
        </div>
    )
}

export default Spinner
