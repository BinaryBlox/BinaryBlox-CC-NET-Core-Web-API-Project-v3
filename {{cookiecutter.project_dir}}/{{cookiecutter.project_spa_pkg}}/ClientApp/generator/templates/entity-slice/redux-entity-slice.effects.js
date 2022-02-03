module.exports = (entitySlice, storePath) => ({
  content: `// Generated with util/create-slice.js

  import axios from 'axios';
import { AppThunk } from '${storePath}'; 
import { actions } from './${entitySlice.charAt(0).toUpperCase() + entitySlice.slice(1)}.slice'
 
export const fetch${entitySlice}s = (): AppThunk => async dispatch => {
    try {
        console.log("Retrieving ${entitySlice}s data...")
        dispatch(actions.get${entitySlice}s())
        const result = await axios("https://api.tvmaze.com/search/shows?q=snow");

        dispatch(actions.get${entitySlice}sSuccess(result.data))
    } catch (err) {
        dispatch(actions.get${entitySlice}sFailure(err))
    }
}
   
`,
  extension: `.effects.ts`
});
