import { AppThunkAction } from "../";

export const REQUEST_INITIAL_POKEMON = 'REQUEST_INITIAL_POKEMON'
export const RECEIVE_INITIAL_POKEMON = 'RECEIVE_INITIAL_POKEMON'

export interface PokedexState {
    isLoading: boolean;
    index?: number;
    pokemons: Pokemon[];
}

export interface Pokemon {
    name: string;
    photo: string;
    type: any[];
}

interface RequestInitialPokemonAction {
    type: typeof REQUEST_INITIAL_POKEMON,
    index: number;
}

interface ReceiveInitialPokemonAction {
    type: typeof RECEIVE_INITIAL_POKEMON,
    index: number;
    pokemons: Pokemon[];
}

export type KnownAction = RequestInitialPokemonAction
    | ReceiveInitialPokemonAction;

export const actionCreators = {
    requestInitialPokemon: (index: number): AppThunkAction<KnownAction> => (dispatch, getState) => {
        const appState = getState();
        if (appState && appState.pokedex && index !== appState.pokedex.index) {
            let url = 'https://pokeapi.co/api/v2/pokemon?limit=300&offset=100';
            fetch(url)
                .then(response => response.json())
                .then(data => {
                    dispatch({
                        type: RECEIVE_INITIAL_POKEMON,
                        index: index,
                        pokemons: data.results
                    });
                });
            dispatch({
                type: REQUEST_INITIAL_POKEMON,
                index: index
            });

        }
    }

}