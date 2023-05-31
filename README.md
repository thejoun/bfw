# BFW

Fully on-chain game prototype inspired by Battle for Wesnoth

### How to deploy locally

#### Requirements

- git
- foundry (forge, anvil, cast)
- nodejs, npm (`apt install nodejs`, `npm`)
- yarn (`npm install yarn --global`)

Update yarn to latest version (>=16):
- `npm install -g n`
- `n lts`
- `n prune`

#### Install

Refresh node modules: 
`yarn install --check-files`.

#### Run

Run `yarn dev` in the `bfw/game` directory.