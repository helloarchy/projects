import Link from 'next/link'
import { GetServerSideProps, InferGetServerSidePropsType } from 'next'

import Layout from '../components/Layout'
import { Button, Container, Grid, Text } from '@nextui-org/react'
import { Project } from '../types/project'
import ProjectCard from '../components/ProjectCard'

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
          <Text h2>Projects list...</Text>
          <Grid.Container
            gap={2}
            justify={'center'}
          >
            {projects.map((project: Project) => (
              <Grid
                xs={12}
                sm={4}
                key={project.id}
              >
                <ProjectCard project={project} />
              </Grid>
            ))}
          </Grid.Container>
        </Container>
      ) : (
        <div>
          <p>No projects</p>
          <Button>Next UI test</Button>
        </div>
      )}
    </Layout>
  )
}

export default ProjectsPage
