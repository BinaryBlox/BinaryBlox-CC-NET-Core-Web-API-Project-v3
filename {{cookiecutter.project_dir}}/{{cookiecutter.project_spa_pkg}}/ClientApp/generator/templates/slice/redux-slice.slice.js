module.exports = (slice, storePath) => ({
  content: `// Generated with util/create-slice.js
import React from "react";
 
import { createSlice, PayloadAction, createEntityAdapter, EntityState, EntityAdapter } from '@reduxjs/toolkit'
import axios from 'axios';
import { AppThunk, RootState } from '${storePath}'; 
 
export interface ${slice} {
    id: Number | string,
    name: string,
    description: string, 
    items: Array<any>,
    status: string,
}

export interface ${slice}State extends EntityState<${slice}> {
    selectedEntityId: number | null;
    selectedEntityTypeId: number | null;
    allItemsLoaded: boolean | null;
    isLoading: boolean;
    isUpdating: boolean;
    statusMessage: string;
    error: any;
};

// Since we don't provide \`selectId\`, it defaults to assuming \`entity.id\` is the right field...

// The magic line  
const entityAdapter: EntityAdapter<${slice}> = createEntityAdapter<${slice}>({
    //selectId: ${slice.charAt(0).toLowerCase() + slice.slice(1)} => ${slice.charAt(0).toLowerCase() + slice.slice(1)}.id,
    // Keep the "all IDs" array sorted based on some value
    //sortComparer: (a, b) => a.someValue.localeCompare(b.someValue)
})


export const initialState: ${slice}State = entityAdapter.getInitialState(<${slice}State>{
    ids: [],
    selectedEntityId: 0,
    selectedEntityTypeId: 0,
    allItemsLoaded: false,
    isLoading: false,
    isUpdating: false,
    statusMessage: null,
    error: null
});
 
// Entity Reducer
const slice = createSlice({
    name: '${slice.charAt(0).toLowerCase() + slice.slice(1)}s',
    initialState,
    reducers: {
        get${slice}s(state: ${slice}State) {
            state = { ...state, statusMessage: "fetching" };
            return state;
        },
        get${slice}sSuccess(state: ${slice}State, action: PayloadAction<any[]>) {
            state = { ...state, isLoading: false, error: null, statusMessage: "success" };
            //console.log(\`State for adapter \${JSON.stringify(action.payload, null, '\t')}\`)
            console.log(\`State for ${slice.toLowerCase()}s adapter success\`);
            //entityAdapter.setAll(state, action.payload)

            var ${slice.charAt(0).toLowerCase() + slice.slice(1)}s: Array<${slice}> = []
                action.payload.forEach(element => {

                    let value = <${slice}>{

                        id: element.${slice.charAt(0).toLowerCase() + slice.slice(1)}.id,
                        name: element.${slice.charAt(0).toLowerCase() + slice.slice(1)}.name,
                        description: element.${slice.charAt(0).toLowerCase() + slice.slice(1)}.description, 
                        items: element.${slice.charAt(0).toLowerCase() + slice.slice(1)}.items,
                        status: element.${slice.charAt(0).toLowerCase() + slice.slice(1)}.status

                    }
                    ${slice.charAt(0).toLowerCase() + slice.slice(1)}s.push(value);
                }
            )  
            return entityAdapter.setAll(state, ${slice.charAt(0).toLowerCase() + slice.slice(1)}s)
        },
        get${slice}sFailure(state: ${slice}State, action: PayloadAction<string>) {
            console.log(\`Error State for adapter \${JSON.stringify(action.payload, null, '\t')}\`)
            state = { ...state, isLoading: false, error: action.payload, statusMessage: "failure" };
            return state;
        }
    },
})
 
export const get${slice}s = ( text: string): AppThunk => async dispatch => {}
 
// Entity Actions
export const { actions } = slice;

// Entity Reducer
export const reducer = slice.reducer;
 
// Entity Selectors
export const entitySelectors = entityAdapter.getSelectors<RootState>(state => state.${slice.charAt(0).toLowerCase() + slice.slice(1)})

export const {
    selectAll: selectAll${slice}s,
    selectById: select${slice}ById,
    selectIds: select${slice}Ids,
    selectTotal: selectTotal${slice}s,
    selectEntities
} = entitySelectors;

export const select${slice}sLoaded = state => state.${slice.charAt(0).toLowerCase() + slice.slice(1)}s.allItemsLoaded; 

export default slice;
`,
  extension: `.slice.ts`
});
