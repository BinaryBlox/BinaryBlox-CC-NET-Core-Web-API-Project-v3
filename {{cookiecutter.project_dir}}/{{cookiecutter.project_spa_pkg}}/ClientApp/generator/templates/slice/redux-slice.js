module.exports = (slice, storePath) => ({
    content: `// Generated with util/create-slice.js
import {reducer} from './${slice.charAt(0).toUpperCase() + slice.slice(1)}.slice'
import * as effects from './${slice.charAt(0).toUpperCase() + slice.slice(1)}.effects'
 
export {
    reducer,
    effects 
};
 
  `,
    extension: `.ts`
  });
  