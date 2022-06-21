import { Text } from '@nextui-org/react'
import { GetServerSideProps, InferGetServerSidePropsType } from 'next'
import { serialize } from 'next-mdx-remote/serialize'
import { MDXRemote, MDXRemoteSerializeResult } from 'next-mdx-remote'

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

  return {
    props: {
      data,
    },
  }
}

const ProjectPage = (
  { data }: InferGetServerSidePropsType<typeof getServerSideProps>,
) => {
  const project = data?.project

  return (
    <>
      <Text>This is the project page for project {project?.title ?? null} with id {project?.id ?? null}</Text>

      <div className={'wrapper'}>
        <MDXRemote {...data.fullDescriptionMdx} lazy />
      </div>
    </>
  )
}

export default ProjectPage
