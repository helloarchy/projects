import { Button } from '@nextui-org/react'
import Link from 'next/link'
import React from 'react'

const Navigation = () => {
  return (
    <nav>
      <Button.Group
        color="gradient"
        ghost
      >
        <Link href="/">
          <Button>Home</Button>
        </Link>
        <Link href="/about">
          <Button>About</Button>
        </Link>
        <Link href="/projects">
          <Button>Projects</Button>
        </Link>
      </Button.Group>
    </nav>
  )
}

export default Navigation
