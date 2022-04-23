import { render, screen } from "@testing-library/react";
import ProjectsPage from "../projects";
import "@testing-library/jest-dom";

const mockFetch = global.fetch = jest.fn(() =>
  Promise.resolve({
    json: () => Promise.resolve([
      { title: "Project 1" },
      { title: "Project 2" }
    ])
  })
) as jest.Mock;

describe("Projects Page", () => {
  afterEach(() => {
    mockFetch.mockReset();
  });

  it("renders a heading", () => {
    render(<ProjectsPage />);

    const heading = screen.getByRole("heading", {
      name: /Projects/i
    });

    expect(heading).toBeInTheDocument();
  });

  it("lists all projects", () => {
    render(<ProjectsPage />);

    const project1 = screen.getByRole("listitem", {
      name: /Project 1/i
    });

    const project2= screen.getByRole("listitem", {
      name: /Project 2/i
    });

    expect(project1).toBeInTheDocument();
    expect(project2).toBeInTheDocument();
  });
});