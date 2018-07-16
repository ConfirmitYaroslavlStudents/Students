const defaultConfig = {
  comparison: {
    scaleToSameSize: true,
    ignore: "antialiasing",
    maxMisMatchPercentage: 0
  },

  output: {
    fallenTestSaveStrategy: "separate",
    createThumbnails: false,

    difference: {
      errorType: "movementDifferenceIntensity",
      transparency: 0.95,
      largeImageThreshold: 0,
      useCrossOrigin: false,
      outputDiff: true,

      errorColor: {
        red: 255,
        green: 0,
        blue: 0
      }
    }
  }
}

module.exports = defaultConfig