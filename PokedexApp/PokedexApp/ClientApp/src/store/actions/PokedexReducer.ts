import { Action, Reducer } from 'redux';
import {
    PokedexState, KnownAction,
    REQUEST_INITIAL_POKEMON,
    RECEIVE_INITIAL_POKEMON,
    RECEIVE_FULL_POKEMON,
    RECEIVE_DETAILED_POKEMON
} from './PokedexActions';

const initialState: PokedexState = {
    isLoading: false,
    pokemons: [],
    count: 0,
    next: '',
    previous: ''
};

export const reducer: Reducer<PokedexState>
    = (state: PokedexState | undefined = initialState, incomingAction: Action)
        : PokedexState => {
        const action = incomingAction as KnownAction;
        switch (action.type) {
            case REQUEST_INITIAL_POKEMON:
                return {
                    ...state,
                    index: action.index,
                    isLoading: true,
                    pokemons: state.pokemons
                }
            case RECEIVE_DETAILED_POKEMON:
                return {
                    ...state,
                    pokemonDetail: action.pokemon
                }
            case RECEIVE_INITIAL_POKEMON:
                console.log('RECEIVE_INITIAL_POKEMON', action)
                if (action.index === state.index) {
                    action.pokemons.forEach(i => i.photo = '/img/Spinner-1s-200px.gif')
                    return {
                        index: action.index,
                        pokemons: action.pokemons,
                        isLoading: false,
                        count: action.count,
                        next: action.next,
                        previous: action.previous,                    };
                }
            case RECEIVE_FULL_POKEMON:
                {
                    let pokemon = state.pokemons.map(i => {
                        if(i.id === action.pokemon.id){
                            i.photo = action.pokemon.photo;
                            i.types = action.pokemon.types; 
                        }
                        return i;
                    })
                    
                    return {
                        ...state,
                        index: state.index,
                        isLoading: state.isLoading,
                        pokemons: pokemon
                    }
                }
        }
        return state;
    };