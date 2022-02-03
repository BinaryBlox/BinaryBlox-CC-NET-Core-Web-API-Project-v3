module.exports = (slice, storePath) => ({
  content: `// Generated with util/create-slice.js

  import axios from 'axios';
import { AppThunk } from '${storePath}'; 
import { actions } from './${slice.charAt(0).toLowerCase() + slice.slice(1)}.reducer'
 
export const fetch${slice}s = (): AppThunk => async dispatch => {
    try {
        console.log("Retrieving ${slice}s data...")
        dispatch(actions.get${slice}s())
        const result = await axios("https://api.tvmaze.com/search/shows?q=snow");

        dispatch(actions.get${slice}sSuccess(result.data))
    } catch (err) {
        dispatch(actions.get${slice}sFailure(err))
    }
}
   
`,
  extension: `.effects.ts`
});
