// Generated with util/create-slice.js

  import axios from 'axios';
import { AppThunk } from '../../../../store'; 
import { actions } from './ApplicationClientViewModel.slice'
import {AccountApplicationApi} from '../../../../lib/account/api'

export const fetchApplicationClientViewModels = (): AppThunk => async dispatch => {
    try {
        console.log("Retrieving ApplicationClientViewModels data...")
        dispatch(actions.getApplicationClientViewModels())
        const result = await axios("https://api.tvmaze.com/search/shows?q=snow");

        dispatch(actions.getApplicationClientViewModelsSuccess(result.data))
    } catch (err) {
        dispatch(actions.getApplicationClientViewModelsFailure(err))
    }
}
   
