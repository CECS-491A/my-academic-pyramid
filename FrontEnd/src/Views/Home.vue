<template>
<div class="team">
  <h1>{{info}}</h1>
  <v-container class="my-5">

    <v-lay row wrap>
      <v-flex xs12 sm6 md4 lg3 v-for="person in team" :key="person.name">
        <v-card flat class="text-xs-center ma-3">
          <v-card-text>
            <div class="subheading">{{person.name}}</div>
            <div class="grey--text">{{person.role}}</div>
          </v-card-text>
        </v-card>
      </v-flex>
    </v-lay>

  </v-container>
</div>
</template>

<script>
//import axios from 'axios'
export default {
  data () {
    return {
      info: "My Academic Pyramid Homepage",
      team: [
        { name: "Krystal Leon", role: 'Scrum Manager'},
        { name: "Hyunwoo Kim", role: 'Fullstack developer'},
        { name: "Luis Julian", role: 'Fullstack developer'},
        { name: "Arturo Peña Contreras", role: 'Fullstack developer'},
        { name: "Trong Nguyen", role: 'Fullstack developer'},
        { name: "Victor Kim", role: 'Fullstack developer'}
      ]
    }
  }
  
}
</script>
<style>
.team {
  background-image: url('../assets/csulb_pyramid.jpg');
  width: 2000px;
  margin: auto;
}
.team h1 {
  font-size:30pt;
  color:blanchedalmond;
}
</style>
