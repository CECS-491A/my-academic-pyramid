
<template>
  <v-dialog v-model="dialog" title="Create New Conversation">
    <v-app>
      <v-card>
        <v-toolbar>
          <v-toolbar-title>New Conversation</v-toolbar-title>
        </v-toolbar>
        <v-card-text>
          <form>
            <v-text-field
              id="UserName"
              v-model="formData.UserName"
              :error-messages="emailErrors"
              label="UserName (Email)"
              required
              @input="$v.formData.UserName.$touch()"
              @blur="$v.formData.UserName.$touch()"
            ></v-text-field>
			 </form>
        </v-card-text>
      </v-card>
    </v-app>
  </v-dialog>
</template>
			