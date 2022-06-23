import { MDXRemote, MDXRemoteSerializeResult } from 'next-mdx-remote'
import { GetServerSideProps, InferGetServerSidePropsType } from 'next'
import { Container, Link, Text } from '@nextui-org/react'
import { serialize } from 'next-mdx-remote/serialize'
import { useRouter } from 'next/router'
import NextLink from 'next/link'

import { Project } from '../../types/project'

type Data = {
  project: Project | null
  fullDescriptionMdx: MDXRemoteSerializeResult | null
}

export const getServerSideProps: GetServerSideProps = async (context) => {
  const { id } = context.query

  console.dir(context.query, { depth: null })

  let data: Data = {
    project: null,
    fullDescriptionMdx: null,
  }

  if (id) {
    try {
      const endpoint = `${process.env.GATEWAY}/api/project/${id}`
      console.log(`Sending request to: ${endpoint}`)

      const res = await fetch(endpoint)

      const project = await res.json()
      console.dir(project, { depth: null })

      const mdx = await serialize(project.fullDescriptionMdx)

      data.project = project
      data.fullDescriptionMdx = mdx

      console.log('Fetched data.')
    } catch (e) {
      console.log(e)
    }
  }

  return {
    props: {
      data,
    },
  }
}

const ProjectPage = ({
  data,
}: InferGetServerSidePropsType<typeof getServerSideProps>) => {
  const project: Project = data?.project
  const router = useRouter()

  if (!project?.id) {
    const { title } = router.query
    return (
      <Container>
        <Text>
          Project {`'${title}'`} not found.
          <NextLink href={'/projects'}>
            <Link>Return to all projects</Link>
          </NextLink>
        </Text>
      </Container>
    )
  }

  return (
    <>
      <Text>
        This is the project page for project {project?.title ?? '[not found]'}{' '}
        with id {project?.id ?? '[not found]'}
      </Text>

      <Container>
        <MDXRemote {...data.fullDescriptionMdx} lazy />
      </Container>
    </>
  )
}

export default ProjectPage
