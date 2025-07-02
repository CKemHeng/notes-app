import { ref, onMounted } from 'vue'
import dayjs from 'dayjs'
import utc from 'dayjs/plugin/utc'
import timezone from 'dayjs/plugin/timezone'

import {
  fetchNotes,
  createNote,
  updateNote,
  deleteNote,
  type Note
} from '../api/noteapi'

export interface CreateNoteRequest {
  title: string;
  content?: string;
}

export function useNotes() {
  const notes = ref<Note[]>([])
  const form = ref({ title: '', content: '', updateAt: new Date() })
  const editingNoteId = ref<number | null>(null)
  const message = ref('')

  async function loadNotes() {
    try {
      const res = await fetchNotes()
      notes.value = res.data
    } catch (e) {
      console.error('Failed to load notes', e)
    }
  }

 async function addOrUpdateNote() {
  try {
    if (editingNoteId.value !== null) {
      await updateNote(editingNoteId.value, {
        id: editingNoteId.value,
        title: form.value.title,
        content: form.value.content
      })

      // Optimized local update (no full reload)
      const index = notes.value.findIndex(n => n.id === editingNoteId.value)
      if (index !== -1) {
        notes.value[index] = {
          ...notes.value[index],
          title: form.value.title,
          content: form.value.content,
          updatedAt: form.value.updateAt // Update the timestamp locally
      
        }
      }

      message.value = 'Note updated successfully!'
      editingNoteId.value = null
    } else {
      const response = await createNote({
        title: form.value.title,
        content: form.value.content,
      })

      // Push newly created note (optional: load just created note from backend if needed)
      await loadNotes() // you may replace this later with a smarter add
      message.value = 'Note created successfully!'
    }

    form.value.title = ''
    form.value.content = ''

    setTimeout(() => (message.value = ''), 3000)
  } catch (e) {
    console.error('Failed to save note', e)
  }
}


  async function removeNote(id: number) {
    try {
      await deleteNote(id)
      notes.value = notes.value.filter((n) => n.id !== id)
      if (editingNoteId.value === id) cancelEdit()
    } catch (e) {
      console.error('Failed to delete note', e)
    }
  }

  function startEdit(note: Note) {
    form.value.title = note.title
    form.value.content = note.content
    editingNoteId.value = note.id
  }

  function cancelEdit() {
    editingNoteId.value = null
    form.value.title = ''
    form.value.content = ''
  }

dayjs.extend(utc)
dayjs.extend(timezone)

function formatDate(dateInput: string | Date | null): string {
  if (!dateInput) return ''

  // If string and no timezone info, append 'Z' (assume UTC)
  let dateStr = typeof dateInput === 'string' ? dateInput : dateInput.toISOString()

  if (typeof dateInput === 'string' && !dateInput.endsWith('Z') && !dateInput.includes('+')) {
    dateStr += 'Z' // treat as UTC
  }

  return dayjs(dateStr)
    .tz('Asia/Phnom_Penh')
    .format('YYYY-MM-DD hh:mm A')
}



  

  onMounted(loadNotes)

  return {
    notes,
    form,
    editingNoteId,
    message,
    addOrUpdateNote,
    removeNote,
    startEdit,
    cancelEdit,
    formatDate,
  }
}
