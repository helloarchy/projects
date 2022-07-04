import Link from 'next/link'
import React from 'react'

const Navigation = () => {
  return (
    <>
      <nav>
        <Link href="/pages">
          <a>Home</a>
        </Link>{' '}
        |{' '}
        <Link href="/pages/about">
          <a>About</a>
        </Link>{' '}
        |{' '}
        <Link href="/pages/projects">
          <a>Projects List</a>
        </Link>{' '}
        | <a href="/api/users">Users API</a>
      </nav>
    </>
  )
}

export default Navigation
