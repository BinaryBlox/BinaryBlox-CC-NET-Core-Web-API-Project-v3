// Generated with util/create-slice.js

import axios from '../../../../utils/axios.util';
import { AppThunk } from '../../../'; 
import { actions } from './MenuData.slice'
 
export const fetchMenuDatas = (): AppThunk => async dispatch => {
    try {
        console.log("Retrieving MenuDatas data...")
        dispatch(actions.getMenuDatas())
         const response = await axios('/api/dashboard/menudata/sidebar');
        //const result = await axios.get<{ menuData: any[]; }>('/api/dashboard/menudata/sidebar');
      // console.log(`Uh oh${JSON.stringify(response, null, '\t')}`)
        dispatch(actions.getMenuDatasSuccess(response.data))
    } catch (err) {
        dispatch(actions.getMenuDatasFailure(err))
    }
}
   
