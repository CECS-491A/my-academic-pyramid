


<template>
  <v-app>
    <NavBarComp></NavBarComp>
    
    <v-content>
      <keep-alive>
      <router-view />
      </keep-alive> 
    </v-content>
  </v-app>
  
</template>

<script>
import NavBarComp from "@/components/NavBarComp"
// TODO figure out how to make a toolbar component.
export default {
  name: 'App',
  components: {
    NavBarComp
  }
}
</script>
