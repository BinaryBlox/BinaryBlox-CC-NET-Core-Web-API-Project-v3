// Generated with util/create-slice.js
import React from "react";
 
import { createSlice, PayloadAction, createEntityAdapter, EntityState, EntityAdapter } from '@reduxjs/toolkit'
import axios from 'axios';
import { AppThunk, RootState } from '../../../'; 
 
export interface MenuData {
    id: Number | string,
    name: string,
    description: string, 
    items: Array<any>,
    status: string,
}

export interface MenuDataState extends EntityState<MenuData> {
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
const entityAdapter: EntityAdapter<MenuData> = createEntityAdapter<MenuData>({
    //selectId: menuData => menuData.id,
    // Keep the "all IDs" array sorted based on some value
    //sortComparer: (a, b) => a.someValue.localeCompare(b.someValue)
})


export const initialState: MenuDataState = entityAdapter.getInitialState(<MenuDataState>{
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
    name: 'menuDatas',
    initialState,
    reducers: {
        getMenuDatas(state: MenuDataState) {
            state = { ...state, statusMessage: "fetching" };
            return state;
        },
        getMenuDatasSuccess(state: MenuDataState, action: PayloadAction<any[]>) {
            state = { ...state, isLoading: false, error: null, statusMessage: "success" };
            console.log(`State for adapter ${JSON.stringify(action.payload, null, '	')}`)
            console.log(`State for menudatas adapter success`);
            //entityAdapter.setAll(state, action.payload)

            var menuDatas: Array<MenuData> = []
                action.payload.forEach(element => {

                    let value = <MenuData>{

                        id: element.id,
                        name: element.name,
                        description: element.description, 
                        items: element.items,
                        status: element.status

                    }
                    menuDatas.push(value);
                }
            )  
            return entityAdapter.setAll(state, menuDatas)
        },
        getMenuDatasFailure(state: MenuDataState, action: PayloadAction<string>) {
            console.log(`Error State for adapter ${JSON.stringify(action.payload, null, '	')}`)
            state = { ...state, isLoading: false, error: action.payload, statusMessage: "failure" };
            return state;
        }
    },
})
 
export const getMenuDatas = ( text: string): AppThunk => async dispatch => {}
 
// Entity Actions
export const { actions } = slice;

// Entity Reducer
export const reducer = slice.reducer;
 
// Entity Selectors
export const entitySelectors = entityAdapter.getSelectors<RootState>(state => state.menuData)

export const {
    selectAll: selectAllMenuDatas,
    selectById: selectMenuDataById,
    selectIds: selectMenuDataIds,
    selectTotal: selectTotalMenuDatas,
    selectEntities
} = entitySelectors;

export const selectMenuDatasLoaded = state => state.menuDatas.allItemsLoaded; 

export default slice;
