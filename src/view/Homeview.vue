<script lang="ts" setup>
import { useNotes } from '../composables/useNotes'

const {
  notes,
  form,
  editingNoteId,
  message,
  filteredNotes, 
  searchQuery,
  filterMode,
  sortMode,
  addOrUpdateNote,
  removeNote,
  startEdit,
  cancelEdit,
  formatDate,
} = useNotes()
</script>


<style scoped>
/* Scrollbar styling for notes list */
section::-webkit-scrollbar {
  width: 8px;
}
section::-webkit-scrollbar-track {
  background: #f0f0f0;
  border-radius: 8px;
}
section::-webkit-scrollbar-thumb {
  background-color: #a78bfa;
  border-radius: 8px;
}
</style>



<template>
<div class="min-h-screen bg-gray-100 flex flex-col items-stretch p-8">
    <h1 class="text-4xl font-extrabold mb-12 text-gray-800 tracking-tight uppercase">
       Simple Personal Note 
    </h1>

   <div class="flex flex-col lg:flex-row gap-12 w-full max-w-full">
      <!-- Form section on left -->
      <form
        @submit.prevent="addOrUpdateNote"
        class="bg-white border border-gray-300 rounded-xl p-8 w-full lg:w-1/2 flex flex-col"
      >
        <h2 class="text-xl font-semibold mb-6 text-gray-900 tracking-wide">
          {{ editingNoteId !== null ? 'Edit Note' : 'Create Note' }}
        </h2>

        <input
          v-model="form.title"
          placeholder="Note Title"
          class="mb-4 px-4 py-3 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-indigo-400"
          required
          autocomplete="off"
          spellcheck="false"
        />

        <textarea
          v-model="form.content"
          placeholder="Content . . . . . . . . . ."
          rows="6"
          class="mb-6 px-4 py-3 border border-gray-300 rounded-md resize-none focus:outline-none focus:ring-2 focus:ring-indigo-400"
        ></textarea>

        <div class="flex gap-4">
          <button
            type="submit"
            class="flex-grow bg-indigo-600 text-white font-medium py-3 rounded-md hover:bg-indigo-700 transition"
          >
            {{ editingNoteId !== null ? 'Update' : 'Add' }}
          </button>
          <button
            v-if="editingNoteId !== null"
            type="button"
            @click="cancelEdit"
            class="px-6 py-3 border border-gray-400 rounded-md text-gray-600 hover:bg-gray-200 transition"
          >
            Cancel
          </button>
        </div>

        <p v-if="message" class="mt-5 text-sm text-green-600 font-semibold">
          {{ message }}
        </p>
      </form>


      
      <!-- Notes list on right -->



      <section

      
        class="w-full lg:w-1/2 bg-white border border-gray-300 rounded-xl p-6 overflow-y-auto max-h-[680px]"
      >
      <input
      v-model="searchQuery"
      type="text"
      placeholder="Search notes..."
      class="mb-4 w-full px-4 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-indigo-400" />

  <div class="flex flex-wrap gap-2 mb-4">
    <select
      v-model="filterMode"
      class="px-3 py-2 border border-gray-300 rounded-md text-sm"
    >
      <option value="all">All Notes</option>
      <option value="hasContent">With Content</option>
      <option value="emptyContent">Title Only</option>
    </select>

    <select
      v-model="sortMode"
      class="px-3 py-2 border border-gray-300 rounded-md text-sm"
    >
      <option value="latest">Newest First</option>
      <option value="oldest">Oldest First</option>
      <option value="titleAZ">Title A–Z</option>
      <option value="titleZA">Title Z–A</option>
    </select>
  </div>
  

    
        <ul class="divide-y divide-gray-200">
          <li
           v-for="item in filteredNotes"
            :key="item.id"
            class="py-4 cursor-pointer hover:bg-gray-50 transition flex flex-col"
            @click="startEdit(item)"
          >
            <h3 class="font-semibold text-lg text-gray-800 truncate">
              {{ item.title }}
          
            </h3>
            <p
              class="mt-1 text-gray-600 text-sm whitespace-pre-wrap max-h-24 overflow-hidden"
            >
              {{ item.content }}
            </p>
            <br>



            <div class="mt-2 flex justify-end gap-4">
              <button
                @click="startEdit(item)"
                class="text-indigo-600 hover:text-indigo-800 text-sm font-semibold"
              >
                Edit
              </button>
              <button
                @click="removeNote(item.id)"
                class="text-red-600 hover:text-red-800 text-sm font-semibold"
              >
                Delete
              </button>

      

            </div>

                    
            <div class="mt-2 flex justify-end gap-4">
            <p>{{ formatDate(item.updatedAt ?? item.createdAt) }}</p>




            </div>

          </li>
        </ul>

      <p v-if="filteredNotes.length === 0" class="text-center text-gray-400 mt-20">
          No matching notes found.
        </p>
      </section>
    </div>
  </div>
</template>


