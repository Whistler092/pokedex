import { Action, Reducer } from 'redux';
import {
    PokedexState, KnownAction,
    REQUEST_INITIAL_POKEMON,
    RECEIVE_INITIAL_POKEMON
} from './PokedexActions';

const initialState: PokedexState = {
    isLoading: false,
    pokemons: []
};

export const reducer: Reducer<PokedexState>
    = (state: PokedexState | undefined = initialState, incomingAction: Action)
        : PokedexState => {
        const action = incomingAction as KnownAction;
        switch (action.type) {
            case REQUEST_INITIAL_POKEMON:
                return {
                    index: action.index,
                    isLoading: true,
                    pokemons: state.pokemons
                }
            case RECEIVE_INITIAL_POKEMON:
                if (action.index === state.index) {
                    return {
                        index: action.index,
                        pokemons: action.pokemons,
                        isLoading: false
                    };
                }
        }
        return state;
    };