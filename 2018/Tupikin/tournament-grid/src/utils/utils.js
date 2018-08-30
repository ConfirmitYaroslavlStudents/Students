export function isPowerOfTwo(number) {
  const n = parseInt(number, 10);
  return Math.log2(n) % 1 === 0 && n !== 1;
}
