import React from 'react';
import './NotFound.css'
const ItemNotFound = (props) => {
    return (
        <p className={'notFound'}>
            Ooops! The item you're trying to access doesn't exist
            <span role="img" aria-label="confused face">😕</span>
        </p>
    )
}
 
export default ItemNotFound;