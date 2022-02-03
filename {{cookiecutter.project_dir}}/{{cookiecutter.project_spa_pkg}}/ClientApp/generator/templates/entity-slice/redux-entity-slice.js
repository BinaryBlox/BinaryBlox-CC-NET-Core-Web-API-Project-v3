module.exports = (entitySlice, storePath) => ({
    content: `// Generated with util/create-slice.js
import {reducer} from './${entitySlice.charAt(0).toUpperCase() + entitySlice.slice(1)}.slice'
import * as effects from './${entitySlice.charAt(0).toUpperCase() + entitySlice.slice(1)}.effects'
 
export {
    reducer,
    effects 
};
 
  `,
    extension: `.ts`
  });
  