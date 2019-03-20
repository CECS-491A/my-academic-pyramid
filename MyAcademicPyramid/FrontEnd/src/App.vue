<template>
  <v-app>
    <v-toolbar app>
      <v-toolbar-title class="headline text-uppercase">
        <span class="font-weight-light">My Academic Pyramid</span>
      </v-toolbar-title>
      <v-spacer></v-spacer>
      <v-btn
        flat
        href="https://github.com/vuetifyjs/vuetify/releases/latest"
        target="_blank"
      >
        <span class="mr-2">Latest Release</span>
      </v-btn>
            <v-btn
        flat
        href="/login"
        target="_blank"
      >
        <span class="mr-2">Login</span>
      </v-btn>
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
