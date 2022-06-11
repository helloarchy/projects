import Link from 'next/link'
import React from 'react'

const Navigation = () => {
  return (
    <>
      <nav>
        <Link href='/'>
          <a>Home</a>
        </Link>{' '}
        |{' '}
        <Link href='/about'>
          <a>About</a>
        </Link>{' '}
        |{' '}
        <Link href='/projects'>
          <a>Projects List</a>
        </Link>{' '}
        | <a href='/api/users'>Users API</a>
      </nav>
    </>
  )
}

export default Navigation;