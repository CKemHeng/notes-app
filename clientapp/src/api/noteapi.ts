import axios from 'axios'

const NoteControllerAPI = 'https://heng-noteapi-backend.azurewebsites.net/api/Note';



export interface Note {
  id: number
  title: string
  content: string
  createdAt: Date | null;
  updatedAt: Date | null;
  
}


export interface NoteRequest {
  title: string;
  content: string;
}


export interface UpdateRequest {
  id:number;
  title: string;
  content: string;

  
}


export async function fetchNotes() {
  return axios.get<Note[]>(NoteControllerAPI)
}

export async function createNote(note: NoteRequest) {
  return axios.post(NoteControllerAPI, note)
}

export async function updateNote(id: number, note: UpdateRequest) {
  return axios.put(`${NoteControllerAPI}/${id}`, note)
}


export async function deleteNote(id: number) {
  return axios.delete(`${NoteControllerAPI}/${id}`)
}
