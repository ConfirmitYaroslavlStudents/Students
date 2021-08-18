import React from 'react';
import './NotFound.css'
const NotFound = (props) => {
    return (
        <p className={'notFound'}>
            Ooops! The page you're looking for can't be found
            <span role="img" aria-label="confused face">😕</span>
        </p>
    )
}
 
export default NotFound;