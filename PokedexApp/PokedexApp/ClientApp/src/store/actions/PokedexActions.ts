import { AppThunkAction } from "../";

export const REQUEST_INITIAL_POKEMON = 'REQUEST_INITIAL_POKEMON'
export const RECEIVE_INITIAL_POKEMON = 'RECEIVE_INITIAL_POKEMON'
export const RECEIVE_FULL_POKEMON = 'RECEIVE_FULL_POKEMON'
export const RECEIVE_DETAILED_POKEMON = 'RECEIVE_DETAILED_POKEMON'

export interface PokedexState {
    isLoading: boolean;
    index?: number;
    pokemons: Pokemon[];
    pokemonDetail?: Pokemon;
    count: number,
    next: string,
    previous: string,
}

export interface Pokemon {
    id: string;
    name: string;
    photo: string;
    types: any[];
    evolutionChain: any[];
    height: string;
    weight: string;
    moves: any[];
}

interface ReceiveFullPokemonAction {
    type: typeof RECEIVE_FULL_POKEMON,
    pokemon: any;
}

interface ReceiveDetailedPokemonAction {
    type: typeof RECEIVE_DETAILED_POKEMON,
    pokemon: any;
}

interface RequestInitialPokemonAction {
    type: typeof REQUEST_INITIAL_POKEMON,
    index: number;
    pokemon?: any;
}

interface ReceiveInitialPokemonAction {
    type: typeof RECEIVE_INITIAL_POKEMON,
    index: number;
    pokemons: Pokemon[];
    pokemon?: any;
    count: number,
    next: string,
    previous: string,
}

export type KnownAction = RequestInitialPokemonAction
    | ReceiveInitialPokemonAction
    | ReceiveFullPokemonAction
    | ReceiveDetailedPokemonAction ;

export const actionCreators = {
    requestInitialPokemon: (index: number): AppThunkAction<KnownAction> => (dispatch, getState) => {
        const appState = getState();
        if (appState && appState.pokedex && index !== appState.pokedex.index) {
            let url = `/api/Poke?offset=${index}`;
            fetch(url)
                .then(response => response.json())
                .then(data => {
                    console.log('requestInitialPokemon', data);
                    dispatch({
                        type: RECEIVE_INITIAL_POKEMON,
                        index: index,
                        count: data.count,
                        next: data.next,
                        previous: data.previous,
                        pokemons: data.results
                    });
                    data.results.forEach((poke: { id: any; }) => {
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
    },
    requestPokemonDetail :  (id: number): AppThunkAction<KnownAction> => (dispatch, getState) => {
        const appState = getState();
        if (appState && appState.pokedex) {
            let url = `/api/Poke/${id}?loadEvolutionChain=true`;
            fetch(url)
                .then(response => response.json())
                .then(pokeData => {
                    //console.log('requestPullPokemon', pokeData);
                    dispatch({
                        type: RECEIVE_DETAILED_POKEMON,
                        pokemon: pokeData
                    });
                });          
        }
    },
    searchPokemon :  (name: string): AppThunkAction<KnownAction> => (dispatch, getState) => {
        const appState = getState();
        if (appState && appState.pokedex) {
            let url = `/api/Poke/byName?search=${name}`;
            fetch(url)
                .then(response => response.json())
                .then(pokeData => {
                    //console.log('requestPullPokemon', pokeData);
                    window.location.href = "/pokemon-details/"+ pokeData.id;

                });          
        }
    },
}