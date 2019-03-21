<template>
  <v-app>
    <v-toolbar app>
      <v-toolbar-title class="headline text-uppercase">
        <span class="font-weight-light">My Academic Pyramid</span>
      </v-toolbar-title>
      <v-spacer></v-spacer>
      <v-btn to="/" flat>Home</v-btn>
      <v-btn to="Login" flat>Login</v-btn>
      <v-btn to="Publish" flat>Publish</v-btn>
      <v-btn to="Logging" flat>Logging</v-btn>
      <v-btn to="UserManagement" flat>UM</v-btn>
    </v-toolbar>
    
	<v-content>
    <keep-alive>
		<router-view />
    </keep-alive> 
	</v-content>
  </v-app>
</template>

<script>

export default {
  name: 'App',
}
</script>
