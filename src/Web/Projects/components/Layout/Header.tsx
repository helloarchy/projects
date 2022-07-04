import React, { ReactNode } from 'react'
import { Container, Grid, Text } from '@nextui-org/react'
import Navigation from './Navigation'
import ThemeSwitch from './ThemeSwitch'

const Header = () => {
  return (
    <Container>
      <Grid.Container>
        <Grid xs={3}>
          <Text h1>Archy</Text>
        </Grid>
        <Grid
          xs={7}
          alignItems={'flex-end'}
        >
          <Navigation />
        </Grid>
        <Grid
          xs={2}
          alignItems={'flex-end'}
          justify={'flex-end'}
        >
          <ThemeSwitch />
        </Grid>
      </Grid.Container>
      <hr />
    </Container>
  )
}

export default Header
