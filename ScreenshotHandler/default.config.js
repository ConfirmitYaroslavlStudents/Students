const defaultConfig = {
  comparison: {
    scaleToSameSize: false,
    ignore: "antialiasing",
    maxMisMatchPercentage: 0
  },

  output: {
    fallenTestSaveStrategy: "separate",
    createThumbnails: false,

    difference: {
      errorType: "movementDifferenceIntensity",
      transparency: 1,
      largeImageThreshold: 0,
      useCrossOrigin: false,
      outputDiff: true,

      errorColor: {
        red: 255,
        green: 0,
        blue: 0
      }
    }
  },

  metadataURL: './.metadata.json'
}

module.exports = defaultConfig