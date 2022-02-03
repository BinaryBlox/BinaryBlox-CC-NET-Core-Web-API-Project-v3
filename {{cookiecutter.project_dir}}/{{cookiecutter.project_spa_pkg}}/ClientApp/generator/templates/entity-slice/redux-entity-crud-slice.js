module.exports = (entitySlice, storePath) => ({
    content: `// Generated with util/create-slice.js
  import React from "react";
  import { createSlice, PayloadAction, createEntityAdapter, EntityState, EntityAdapter } from '@reduxjs/toolkit'
  import axios from 'axios';
  import { AppThunk, RootState } from '${storePath}'; 
   
  
  export interface ${entitySlice}State extends EntityState<${entitySlice}> { 
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
  const entityAdapter: EntityAdapter<${entitySlice}> = createEntityAdapter<${entitySlice}>({
      //selectId: ${entitySlice.charAt(0).toLowerCase() + entitySlice.slice(1)} => ${entitySlice.charAt(0).toLowerCase() + entitySlice.slice(1)}.id,
      // Keep the "all IDs" array sorted based on some value
      //sortComparer: (a, b) => a.someValue.localeCompare(b.someValue)
  })
   
  export const initialState: ${entitySlice}State = entityAdapter.getInitialState(<${entitySlice}State>{
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
  const entitySlice = createSlice({
      name: '${entitySlice.charAt(0).toLowerCase() + entitySlice.slice(1)}s',
      initialState,
      reducers: {
          get${entitySlice}s(state: ${entitySlice}State) {
              state = { ...state, statusMessage: "fetching" };
              return state;
          },
          get${entitySlice}sSuccess(state: ${entitySlice}State, action: PayloadAction<any[]>) {
              state = { ...state, isLoading: false, error: null, statusMessage: "success" };
              //console.log(\`State for adapter \${JSON.stringify(action.payload, null, '\t')}\`)
              console.log(\`State for ${entitySlice.toLowerCase()}s adapter success\`);
              //entityAdapter.setAll(state, action.payload)
  
              var ${entitySlice.charAt(0).toLowerCase() + entitySlice.slice(1)}s: Array<${entitySlice}> = []
                  action.payload.forEach(element => {
  
                      let value = <${entitySlice}>{
  
                          id: element.${entitySlice.charAt(0).toLowerCase() + entitySlice.slice(1)}.id,
                          name: element.${entitySlice.charAt(0).toLowerCase() + entitySlice.slice(1)}.name,
                          description: element.${entitySlice.charAt(0).toLowerCase() + entitySlice.slice(1)}.description, 
                          items: element.${entitySlice.charAt(0).toLowerCase() + entitySlice.slice(1)}.items,
                          status: element.${entitySlice.charAt(0).toLowerCase() + entitySlice.slice(1)}.status
  
                      }
                      ${entitySlice.charAt(0).toLowerCase() + entitySlice.slice(1)}s.push(value);
                  }
              )  
              return entityAdapter.setAll(state, ${entitySlice.charAt(0).toLowerCase() + entitySlice.slice(1)}s)
          },
          get${entitySlice}sFailure(state: ${entitySlice}State, action: PayloadAction<string>) {
              console.log(\`Error State for adapter \${JSON.stringify(action.payload, null, '\t')}\`)
              state = { ...state, isLoading: false, error: action.payload, statusMessage: "failure" };
              return state;
          }
      },
  })
   
  export const get${entitySlice}s = ( text: string): AppThunk => async dispatch => {}
   
  // Entity Actions
  export const { actions } = entitySlice;
  
  // Entity Reducer
  export const reducer = entitySlice.reducer;
   
  // Entity Selectors
  export const entitySelectors = entityAdapter.getSelectors<RootState>(state => state.${entitySlice.charAt(0).toLowerCase() + entitySlice.slice(1)})
  
  export const {
      selectAll: selectAll${entitySlice}s,
      selectById: select${entitySlice}ById,
      selectIds: select${entitySlice}Ids,
      selectTotal: selectTotal${entitySlice}s,
      selectEntities
  } = entitySelectors;
  
  export const select${entitySlice}sLoaded = state => state.${entitySlice.charAt(0).toLowerCase() + entitySlice.slice(1)}s.allItemsLoaded; 
  
  export default slice;
  `,
    extension: `.slice.ts`
  });
  