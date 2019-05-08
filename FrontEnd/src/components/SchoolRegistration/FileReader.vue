<template>

  <v-btn 
  class="text-reader"
  color="cyan">
    <input type="file" @change="loadTextFromFile">
  </v-btn>
</template>

<script>
export default {
  methods: {
    loadTextFromFile(ev) {
      const file = ev.target.files[0];
      const reader = new FileReader();

      reader.onload = e => this.$emit("load", e.target.result);
      reader.readAsText(file);
    }
  }
};
</script>