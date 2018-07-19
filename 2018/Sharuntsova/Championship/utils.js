shuffleArray = (a) => {
    for (let i = a.length - 1; i > 0; i--) {
        const j = Math.floor(Math.random() * (i + 1));
        [a[i], a[j]] = [a[j], a[i]];
    }
    return a;
}

 isPowerOfTwo = (number) =>{
     return number && (number & (number - 1)) === 0;
 }

// isPowerOfTwo = (number) =>{
//     if (number>1) {
//         if (number%2==0)
//             isPowerOfTwo(number/2);
//         else return false;
//     }
//      return true;   
// }


module.exports = {
    shuffleArray,
    isPowerOfTwo
}
