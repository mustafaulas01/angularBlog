import { Component, OnInit } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ImageService } from './image.service';
import { Observable } from 'rxjs';
import { BlogImage } from '../../models/blogimage.model';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-image-selector',
  standalone: true,
  imports: [FormsModule,ReactiveFormsModule,CommonModule],
  templateUrl: './image-selector.component.html',
  styleUrl: './image-selector.component.css'
})
export class ImageSelectorComponent implements OnInit {

private file ?:File;
fileName:string='';
title:string='';
images$?:Observable<BlogImage[]>;

constructor (private imageService:ImageService) {}

  ngOnInit(): void {
  this.getImages();
  }

  onFileUploadChange(event:Event):void
  {
   const element= event.currentTarget as HTMLInputElement;
   this.file=element.files?.[0];
  }

  uploadImage(): void {

    if (this.file && this.fileName !== '' && this.title !== '') {

      this.imageService.uploadImage(this.file, this.fileName, this.title).subscribe(
        {
          next: (response) => {
            console.log(this.getImages());
            this.getImages();
          }
        }
      )

    }
  }

  private getImages()
  {
    this.images$= this.imageService.getAllImages();
  }

}

