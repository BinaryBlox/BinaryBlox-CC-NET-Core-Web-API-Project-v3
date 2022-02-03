const component = require(".");
  
const sliceEffects = require("./redux-slice.effects");
const sliceReducer = require("./slice.reducer");
const slice = require("./redux-slice");
module.exports = [ 
  slice,
  sliceEffects,
  sliceReducer
];
