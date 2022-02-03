// Generated with util/create-slice.js
import React from "react";
 
import { createSlice, PayloadAction, createEntityAdapter, EntityState, EntityAdapter } from '@reduxjs/toolkit'
import axios from 'axios';
import { AppThunk, RootState } from '../../../../store'; 
 
export interface ApplicationClientViewModel {
    id: Number | string,
    name: string,
    description: string, 
    items: Array<any>,
    status: string,
}

export interface ApplicationClientViewModelState extends EntityState<ApplicationClientViewModel> {
    selectedEntityId: number | null;
    selectedEntityTypeId: number | null;
    allItemsLoaded: boolean | null;
    isLoading: boolean;
    isUpdating: boolean;
    statusMessage: string;
    error: any;
};

// Since we don't provide `selectId`, it defaults to assuming `entity.id` is the right field...

// The magic line  
const entityAdapter: EntityAdapter<ApplicationClientViewModel> = createEntityAdapter<ApplicationClientViewModel>({
    //selectId: applicationClientViewModel => applicationClientViewModel.id,
    // Keep the "all IDs" array sorted based on some value
    //sortComparer: (a, b) => a.someValue.localeCompare(b.someValue)
})


export const initialState: ApplicationClientViewModelState = entityAdapter.getInitialState(<ApplicationClientViewModelState>{
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
    name: 'applicationClientViewModels',
    initialState,
    reducers: {
        getApplicationClientViewModels(state: ApplicationClientViewModelState) {
            state = { ...state, statusMessage: "fetching" };
            return state;
        },
        getApplicationClientViewModelsSuccess(state: ApplicationClientViewModelState, action: PayloadAction<any[]>) {
            state = { ...state, isLoading: false, error: null, statusMessage: "success" };
            //console.log(`State for adapter ${JSON.stringify(action.payload, null, '	')}`)
            console.log(`State for applicationclientviewmodels adapter success`);
            //entityAdapter.setAll(state, action.payload)

            var applicationClientViewModels: Array<any> = []
                action.payload.forEach(element => {

                    let value = <ApplicationClientViewModel>{

                        id: element.applicationClientViewModel.id,
                        name: element.applicationClientViewModel.name,
                        description: element.applicationClientViewModel.description, 
                        items: element.applicationClientViewModel.items,
                        status: element.applicationClientViewModel.status

                    }
                    applicationClientViewModels.push(value);
                }
            )  
            return entityAdapter.setAll(state, applicationClientViewModels)
        },
        getApplicationClientViewModelsFailure(state: ApplicationClientViewModelState, action: PayloadAction<string>) {
            console.log(`Error State for adapter ${JSON.stringify(action.payload, null, '	')}`)
            state = { ...state, isLoading: false, error: action.payload, statusMessage: "failure" };
            return state;
        }
    },
})
 
export const getApplicationClientViewModels = ( text: string): AppThunk => async dispatch => {}
 
// Entity Actions
export const { actions } = slice;

// Entity Reducer
export const reducer = slice.reducer;
 
// Entity Selectors
export const entitySelectors = entityAdapter.getSelectors<RootState>(state => state.applicationClientViewModel)

export const {
    selectAll: selectAllApplicationClientViewModels,
    selectById: selectApplicationClientViewModelById,
    selectIds: selectApplicationClientViewModelIds,
    selectTotal: selectTotalApplicationClientViewModels,
    selectEntities
} = entitySelectors;

export const selectApplicationClientViewModelsLoaded = state => state.applicationClientViewModels.allItemsLoaded; 

export default slice;
