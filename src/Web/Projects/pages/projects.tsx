import Link from "next/link";
import { GetServerSideProps, InferGetServerSidePropsType } from "next";

import Layout from "../components/Layout";

type Project = {
  title: string;
}

type Data = {
  projects: Project[]
}

export const getServerSideProps: GetServerSideProps = async (context) => {
  // Fetch data from external API
  const res = await fetch(`http://localhost:5000/api/projects`);
  const data: Data = await res.json();

  // Pass data to the page via props
  return { props: { data } };
};

function renderProjects(projects: Project[]) {
  {
    if (projects?.length) {
      return (
        <ul>
          {projects.map((project: Project) => (
            <li key={project.title}>{project.title}</li>
          ))}
        </ul>
      );
    } else {
      return <p>No projects</p>;
    }
  }
}

const ProjectsPage = ({ data }: InferGetServerSidePropsType<typeof getServerSideProps>) => (
  <Layout title="Projects | All Projects">
    <h1>Projects</h1>
    <p>This is the page for all projects</p>
    {renderProjects(data.projects)}
    <p>
      <Link href="/">
        <a>Go home</a>
      </Link>
    </p>
  </Layout>
);

export default ProjectsPage;
