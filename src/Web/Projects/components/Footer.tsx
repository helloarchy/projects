import React from 'react'
import { Grid, Spacer, Text } from '@nextui-org/react'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faGithubSquare, faLinkedin } from '@fortawesome/free-brands-svg-icons'
import Link from 'next/link'

const Footer = () => {
  const date = new Date()

  return (
    <>
      <footer>
        <hr />
        <Grid.Container gap={2}>
          <Grid xs={8}>
            <Text h5>&copy; {date.getFullYear()} Robert Hardy</Text>
          </Grid>
          <Grid xs={4} justify={'flex-end'}>
            <Link href={'https://www.linkedin.com/in/archy'}>
              <Text h3>
                <FontAwesomeIcon icon={faLinkedin} />
              </Text>
            </Link>
            <Spacer />
            <Link href={'https://github.com/helloarchy'}>
              <Text h3>
                <FontAwesomeIcon icon={faGithubSquare} />
              </Text>
            </Link>
          </Grid>
        </Grid.Container>
      </footer>
    </>
  )
}

export default Footer
