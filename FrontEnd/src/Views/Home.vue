<template>
<v-app>
  <v-parallax  src="https://cdn.vuetifyjs.com/images/backgrounds/vbanner.jpg">
  <h1>{{info}}</h1>
    <v-layout row wrap>
      <v-flex xs12 sm6 md4 lg3 v-for="person in team" :key="person.name">
        <v-card flat class="text-xs-center ma-3">
          <v-card-text>
            <div class="subheading">{{person.name}}</div>
            <div class="grey--text">{{person.role}}</div>
          </v-card-text>
        </v-card>
      </v-flex>
    </v-layout>
</v-parallax>
<v-layout>
  <v-flex xs12 sm6 offset-sm3>
    <v-card color="cyan lighten-4" >
      <v-card-title primary-title > 
        <h3 class="display-1"> Welcome to My Academic Pyramid application </h3>
        
      </v-card-title>
      <div>{{welcome_text}}</div>
    </v-card>
  </v-flex>


</v-layout>
  <Timeline/>
</v-app>
</template>

<script>
import Timeline from '@/components/Timeline'

export default {
  components:{
    Timeline
  },
  data () {
    return {
      info: "My Academic Pyramid Homepage",
      team: [
        { name: "Krystal Leon", role: 'Team Leader'},
        { name: "Hyunwoo Kim", role: 'Fullstack developer'},
        { name: "Luis Julian", role: 'Fullstack developer'},
        { name: "Arturo Peña Contreras", role: 'Fullstack developer'},
        { name: "Trong Nguyen", role: 'Fullstack developer'},
        { name: "Victor Kim", role: 'Fullstack developer'}
      ],
      welcome_text : " Please login to access to full features"
    }
  }
  
}
</script>
<style>
.team h1 {
  font-size:30pt;
  color:rgb(26, 125, 182);
}
</style>
