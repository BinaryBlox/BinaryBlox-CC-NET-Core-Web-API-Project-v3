const component = require(".");
  
const sliceEffects = require("./redux-entity-slice.effects");
const sliceReducer = require("./redux-entity-slice.slice");
const slice = require("./redux-entity-slice");
module.exports = [ 
  slice,
  sliceEffects,
  sliceReducer
];
