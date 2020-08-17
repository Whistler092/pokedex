import { Action, Reducer } from 'redux';
import {
    PokedexState, KnownAction,
    REQUEST_INITIAL_POKEMON,
    RECEIVE_INITIAL_POKEMON,
    RECEIVE_FULL_POKEMON
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
            case RECEIVE_FULL_POKEMON:
                {
                    //console.log('RECEIVE_FULL_POKEMON', action.pokemon);
                    /* let pokemon = state.pokemons.filter(i => i.id === action.pokemon.id)
                    pokemon[0].photo = action.pokemon.photo;
                    pokemon[0].type = action.pokemon.types; */
                    let pokemon = state.pokemons.map(i => {
                        if(i.id === action.pokemon.id){
                            i.photo = action.pokemon.photo;
                            i.type = action.pokemon.types; 
                        }
                        return i;
                    })
                    
                    return {
                        index: state.index,
                        isLoading: state.isLoading,
                        pokemons: pokemon
                    }
                }
        }
        return state;
    };