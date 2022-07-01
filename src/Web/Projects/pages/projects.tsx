import Link from 'next/link'
import { GetServerSideProps, InferGetServerSidePropsType } from 'next'

import Layout from '../components/Layout'
import { Button, Container, Grid, Text } from '@nextui-org/react'
import { Project } from '../types/project'
import ProjectCard from '../components/ProjectCard'
import NextLink from 'next/link'

type Data = {
  projects: Project[]
}

export const getServerSideProps: GetServerSideProps = async (context) => {
  let projects: Project[] = []
  let data: Data = {
    projects,
  }

  try {
    const endpoint = `${process.env.GATEWAY}/api/project`
    console.log(`Sending request to: ${endpoint}`)

    const res = await fetch(endpoint)
    data.projects = await res.json()

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

const ProjectsPage = ({
  data,
}: InferGetServerSidePropsType<typeof getServerSideProps>) => {
  const projects = data.projects
  return (
    <Layout title="Projects | All Projects">
      {projects?.length && projects?.length > 0 ? (
        <Container>
          <Text h2>Projects</Text>
          <Grid.Container gap={2} justify={'center'} wrap={'wrap'}>
            {projects.map((project: Project) => (
              <Grid xs={12} sm={7} md={5} lg={4} xl={3}>
                <ProjectCard key={project.id} project={project} />
              </Grid>
            ))}
          </Grid.Container>
        </Container>
      ) : (
        <Container>
          <Text h2>No projects</Text>
          <NextLink href={'/'}>
            <Button>Next UI test</Button>
          </NextLink>
        </Container>
      )}
    </Layout>
  )
}

export default ProjectsPage
