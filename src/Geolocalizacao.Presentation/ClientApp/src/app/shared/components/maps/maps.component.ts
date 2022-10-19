import { AfterViewInit, Component, EventEmitter, Input, Output, ViewChild } from '@angular/core';

@Component({
    selector: 'app-maps',
    templateUrl: './maps.component.html',
    styleUrls: ['./maps.component.scss']
})

export class MapsComponent implements AfterViewInit {
  @ViewChild('map_canvas') mapElement;
  @ViewChild('content') contentElement;

  @Input() latitude: number;
  @Input() longitude: number;

  @Input() areas = new Array();
  @Input() areasSelecionadas = new Array();
  @Input() permiteSelecionar = false;

  @Output() areaEventEmitter = new EventEmitter<any>();
  @Output() removerAreaEventEmitter = new EventEmitter<any>();
  
  infoWindow: google.maps.InfoWindow;
  popup: google.maps.OverlayView;

  regiaoSelecionada;
  polygonSelecionado: google.maps.Polygon;
  popupSelecionado: google.maps.OverlayView;

  regioes = new Array();

  permiteEditar = true;
  permiteRecortar = false;

  constructor() {

  }

  ngAfterViewInit() {
    console.log(this.areas)
    if (this.latitude && this.longitude) {
      this.carregarMapa();
    }
  }

  carregarMapa() {
    const map = new google.maps.Map(
      this.mapElement.nativeElement as HTMLElement,
      {
        center: { lat: this.latitude, lng: this.longitude },
        zoom: 10,
        mapTypeId: google.maps.MapTypeId.ROADMAP
      }
    );
    this.infoWindow = new google.maps.InfoWindow();    

    this.areas.forEach(area => {
      this.marcarArea(map, area);
    });

  }

  remover(area) {
    this.removerAreaEventEmitter.emit(area);

    this.popupSelecionado.setMap(null);
    this.polygonSelecionado.setMap(null);
  }

  recortar(area) {
    this.regiaoSelecionada = area;
  }

  marcarArea(map: google.maps.Map, area) {
    area.geometry.coordinates.forEach(poligono => {
      this.adicionarPoligono(map, area, poligono);      
    });   
    
  }

  adicionarPoligono(map: google.maps.Map, area, poligono) {
    const corPadrao = '#FFF';
    const corSelecionado = '#76C9FF';

    let cor = corSelecionado;

    let coordenadasFormatada = new Array();

    poligono.forEach(coordenada => {
      const coordenadaFormatada = { lat: coordenada[1], lng: coordenada[0] }
      coordenadasFormatada.push(coordenadaFormatada);
    });

    let polygon = new google.maps.Polygon({
      paths: coordenadasFormatada,
      strokeColor: "#000",
      strokeOpacity: 0.6,
      strokeWeight: 2,
      fillColor: corPadrao,
      fillOpacity: 0.6,
      map: map,
    });

    const params = {
      area: area,
      polygon: polygon
    };

    this.regioes.push(params);

    polygon.addListener("mouseover", c => {
      polygon.setOptions({ fillColor: corSelecionado });

      //popup.setMap(map);
      this.popupSelecionado = new Popup(
        new google.maps.LatLng(area.latitude, area.longitude),
        this.contentElement.nativeElement as HTMLElement
      );
      this.popupSelecionado.setMap(map);
      this.polygonSelecionado = polygon;

    });

    polygon.addListener("mouseout", c => {
      polygon.setOptions({ fillColor: corPadrao });

      this.popupSelecionado.setMap(null);
    });

    polygon.addListener("click", c => {
      console.log(area)
      //this.areaEventEmitter.emit(area);


      //const texto = `<button onclick="myFunction()">Click me</button>`
      //this.infoWindow.setContent(texto);

      //const coordenada = { lat: area.latitude, lng: area.longitude }

      //this.infoWindow.setPosition(coordenada);

      //this.infoWindow.open(map);


    });
  }
}



export class Popup extends google.maps.OverlayView {
  position: google.maps.LatLng;
  containerDiv: HTMLDivElement;

  constructor(position: google.maps.LatLng, content: HTMLElement) {
    super();
    this.position = position;
    content.classList.add("popup-bubble");

    // This zero-height div is positioned at the bottom of the bubble.
    const bubbleAnchor = document.createElement("div");

    bubbleAnchor.classList.add("popup-bubble-anchor");
    bubbleAnchor.appendChild(content);

    // This zero-height div is positioned at the bottom of the tip.
    this.containerDiv = document.createElement("div");
    this.containerDiv.classList.add("popup-container");
    this.containerDiv.appendChild(bubbleAnchor);

    // Optionally stop clicks, etc., from bubbling up to the map.
    Popup.preventMapHitsAndGesturesFrom(this.containerDiv);
  }


  /** Called when the popup is added to the map. */
  onAdd() {
    this.getPanes()!.floatPane.appendChild(this.containerDiv);
  }

  /** Called when the popup is removed from the map. */
  onRemove() {
    if (this.containerDiv.parentElement) {
      this.containerDiv.parentElement.removeChild(this.containerDiv);
    }
  }

  /** Called each frame when the popup needs to draw itself. */
  draw() {
    const divPosition = this.getProjection().fromLatLngToDivPixel(
      this.position
    )!;

    // Hide the popup when it is far out of view.
    const display =
      Math.abs(divPosition.x) < 4000 && Math.abs(divPosition.y) < 4000
        ? "block"
        : "none";

    if (display === "block") {
      this.containerDiv.style.left = divPosition.x + "px";
      this.containerDiv.style.top = divPosition.y + "px";
    }

    if (this.containerDiv.style.display !== display) {
      this.containerDiv.style.display = display;
    }
  }
}
