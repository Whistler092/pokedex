import { AppThunkAction } from "../";

export const REQUEST_INITIAL_POKEMON = 'REQUEST_INITIAL_POKEMON'
export const RECEIVE_INITIAL_POKEMON = 'RECEIVE_INITIAL_POKEMON'
export const RECEIVE_FULL_POKEMON = 'RECEIVE_FULL_POKEMON'

export interface PokedexState {
    isLoading: boolean;
    index?: number;
    pokemons: Pokemon[];
}

export interface Pokemon {
    id: string;
    name: string;
    photo: string;
    type: any[];
}

interface ReceiveFullPokemonAction {
    type: typeof RECEIVE_FULL_POKEMON,
    pokemon: any;
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
    | ReceiveInitialPokemonAction
    | ReceiveFullPokemonAction;

export const actionCreators = {
    requestInitialPokemon: (index: number): AppThunkAction<KnownAction> => (dispatch, getState) => {
        const appState = getState();
        if (appState && appState.pokedex && index !== appState.pokedex.index) {
            let url = '/api/Poke?offset=100';
            fetch(url)
                .then(response => response.json())
                .then(data => {
                    //console.log('requestInitialPokemon', data);
                    dispatch({
                        type: RECEIVE_INITIAL_POKEMON,
                        index: index,
                        pokemons: data
                    });
                    data.forEach((poke: { id: any; }) => {
                        let url = `/api/Poke/${poke.id}`;
                        fetch(url)
                            .then(response => response.json())
                            .then(pokeData => {
                                //console.log('requestPullPokemon', pokeData);
                                dispatch({
                                    type: RECEIVE_FULL_POKEMON,
                                    pokemon: pokeData
                                });
                            });
                    });
                });
            dispatch({
                type: REQUEST_INITIAL_POKEMON,
                index: index
            });
        }
    }
}