import React from 'react';
import './BadRequest.css'
const badRequest = () => {
    return (
        <p className="badRequest">
            Something's wrong... You might want to try one more time
            <span aria-label="frowning face with open mouth">😦</span>
        </p>
    )
}
 
export default badRequest;